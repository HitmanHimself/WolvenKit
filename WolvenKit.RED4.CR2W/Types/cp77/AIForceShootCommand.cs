using WolvenKit.RED4.CR2W.Reflection;
using FastMember;
using static WolvenKit.RED4.CR2W.Types.Enums;

namespace WolvenKit.RED4.CR2W.Types
{
	[REDMeta]
	public class AIForceShootCommand : AICombatRelatedCommand
	{
		[Ordinal(5)] [RED("targetOverrideNodeRef")] public NodeRef TargetOverrideNodeRef { get; set; }
		[Ordinal(6)] [RED("targetOverridePuppetRef")] public gameEntityReference TargetOverridePuppetRef { get; set; }
		[Ordinal(7)] [RED("duration")] public CFloat Duration { get; set; }

		public AIForceShootCommand(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }
	}
}