using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using WolvenKit.Common.Services;
using WolvenKit.RED4.Types.Exceptions;

namespace WolvenKit.RED4.Archive.IO;

public class ArchiveReader
{
    public EFileReadErrorCodes ReadArchive(string path, IHashService hashService, out Archive ar)
    {
        ar = new Archive()
        {
            ArchiveAbsolutePath = path
        };

        using var fs = new FileStream(ar.ArchiveAbsolutePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        using var mmf = MemoryMappedFile.CreateFromFile(fs, null, 0, MemoryMappedFileAccess.ReadWrite, HandleInheritability.None, false);

        // read header
        uint customDataLength;

        using (var vs = mmf.CreateViewStream(0, Header.EXTENDED_SIZE, MemoryMappedFileAccess.Read))
        using (var br = new BinaryReader(vs))
        {
            ar.Header = ReadHeader(br);
            customDataLength = br.ReadUInt32();
        }

        // read custom
        try
        {
            if (customDataLength != 0)
            {
                using var vs = mmf.CreateViewStream(Header.EXTENDED_SIZE, customDataLength, MemoryMappedFileAccess.Read);
                using var br = new BinaryReader(vs);
                if (br.BaseStream.Length >= LxrsFooter.MIN_LENGTH)
                {
                    var lxrs = ReadLxrsFooter(br);
                    foreach (var s in lxrs.FileInfos)
                    {
                        hashService.AddCustom(s);
                    }
                }
            }
        }
        catch (UnauthorizedAccessException)
        {
            //Console.WriteLine(e);
        }

        // read files
        using (var vs = mmf.CreateViewStream((long)ar.Header.IndexPosition, ar.Header.IndexSize,
        MemoryMappedFileAccess.Read))
        using (var br = new BinaryReader(vs))
        {
            ar.Index = ReadIndex(br, hashService);
        }

        foreach (var file in ar.Index.FileEntries.Values)
        {
            file.Archive = ar;
            ar.Files.Add(file.Key, file);
        }

        return EFileReadErrorCodes.NoError;
    }

    private LxrsFooter ReadLxrsFooter(BinaryReader br)
    {
        var customPaths = new List<string>();
        var footer = new LxrsFooter(customPaths);
        footer.Read(br);

        return footer;
    }

    private Index ReadIndex(BinaryReader br, IHashService hashService)
    {
        var index = new Index
        {
            FileTableOffset = br.ReadUInt32(),
            FileTableSize = br.ReadUInt32(),
            Crc = br.ReadUInt64(),
            FileEntryCount = br.ReadUInt32(),
            FileSegmentCount = br.ReadUInt32(),
            ResourceDependencyCount = br.ReadUInt32()
        };

        // read tables
        for (var i = 0; i < index.FileEntryCount; i++)
        {
            var entry = ReadFileEntry(br, hashService);

            if (!index.FileEntries.ContainsKey(entry.NameHash64))
            {
                index.FileEntries.Add(entry.NameHash64, entry);
            }
            else
            {
                // TODO
            }
        }

        for (var i = 0; i < index.FileSegmentCount; i++)
        {
            index.FileSegments.Add(ReadFileSegment(br));
        }

        for (var i = 0; i < index.ResourceDependencyCount; i++)
        {
            index.Dependencies.Add(ReadDependency(br));
        }

        foreach (var (_, value) in index.FileEntries)
        {
            var startIndex = (int)value.SegmentsStart;
            var nextIndex = (int)value.SegmentsEnd;

            value.Size = index.FileSegments[startIndex].Size;
            value.ZSize = index.FileSegments[startIndex].ZSize;

            for (var j = startIndex + 1; j < nextIndex; j++)
            {
                value.Size += index.FileSegments[j].Size;
                value.ZSize += index.FileSegments[j].ZSize;
            }
        }

        return index;
    }

    private Header ReadHeader(BinaryReader br)
    {
        var header = new Header()
        {
            Magic = br.ReadUInt32(),
            Version = br.ReadUInt32(),
            IndexPosition = br.ReadUInt64(),
            IndexSize = br.ReadUInt32(),
            DebugPosition = br.ReadUInt64(),
            DebugSize = br.ReadUInt32(),
            Filesize = br.ReadUInt64()
        };
        if (header.Magic != Header.MAGIC)
        {
            throw new InvalidParsingException("not an ArchiveHeader");
        }

        return header;
    }

    private Dependency ReadDependency(BinaryReader br) => new(br.ReadUInt64());

    private FileEntry ReadFileEntry(BinaryReader br, IHashService hashService)
    {
        var fileEntry = new FileEntry(hashService)
        {
            NameHash64 = br.ReadUInt64(),
            Timestamp = DateTime.FromFileTime(br.ReadInt64()),
            NumInlineBufferSegments = br.ReadUInt32(),
            SegmentsStart = br.ReadUInt32(),
            SegmentsEnd = br.ReadUInt32(),
            ResourceDependenciesStart = br.ReadUInt32(),
            ResourceDependenciesEnd = br.ReadUInt32(),
            SHA1Hash = br.ReadBytes(20)
        };

        return fileEntry;
    }

    private FileSegment ReadFileSegment(BinaryReader br) => new()
    {
        Offset = br.ReadUInt64(),
        ZSize = br.ReadUInt32(),
        Size = br.ReadUInt32()
    };
}
