using WolvenKit.RED4.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED4.CR2W.Types.Enums;

namespace WolvenKit.RED4.CR2W.Types
{
	[REDMeta]
	public class animComponentTagCondition : animIStaticCondition
	{
		[Ordinal(0)] [RED("animTag")] public CName AnimTag { get; set; }

		public animComponentTagCondition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }
	}
}