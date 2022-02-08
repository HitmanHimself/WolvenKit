
namespace WolvenKit.RED4.Types
{
	[REDMeta]
	public partial class ToggleAOEEffect : ActionBool
	{

		public ToggleAOEEffect()
		{
			RequesterID = new();
			InteractionChoice = new() { CaptionParts = new() { Parts = new() }, Data = new(), ChoiceMetaData = new() { Type = new() }, LookAtDescriptor = new() { Offset = new(), OrbId = new() } };
			ActionWidgetPackage = new() { DependendActions = new() };
			CanTriggerStim = true;
		}
	}
}
