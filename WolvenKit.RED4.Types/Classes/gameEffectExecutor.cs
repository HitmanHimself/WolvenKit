using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	[REDMeta]
	public partial class gameEffectExecutor : gameEffectNode
	{
		[Ordinal(0)] 
		[RED("usesHitCooldown")] 
		public CBool UsesHitCooldown
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}
	}
}
