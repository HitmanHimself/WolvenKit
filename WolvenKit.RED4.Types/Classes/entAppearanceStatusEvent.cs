using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	[REDMeta]
	public partial class entAppearanceStatusEvent : redEvent
	{
		[Ordinal(0)] 
		[RED("status")] 
		public CEnum<entAppearanceStatus> Status
		{
			get => GetPropertyValue<CEnum<entAppearanceStatus>>();
			set => SetPropertyValue<CEnum<entAppearanceStatus>>(value);
		}
	}
}
