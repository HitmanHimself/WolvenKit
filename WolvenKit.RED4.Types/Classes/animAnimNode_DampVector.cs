using static WolvenKit.RED4.Types.Enums;

namespace WolvenKit.RED4.Types
{
	[REDMeta]
	public partial class animAnimNode_DampVector : animAnimNode_VectorValue
	{
		[Ordinal(11)] 
		[RED("defaultIncreaseSpeed")] 
		public Vector4 DefaultIncreaseSpeed
		{
			get => GetPropertyValue<Vector4>();
			set => SetPropertyValue<Vector4>(value);
		}

		[Ordinal(12)] 
		[RED("defaultDecreaseSpeed")] 
		public Vector4 DefaultDecreaseSpeed
		{
			get => GetPropertyValue<Vector4>();
			set => SetPropertyValue<Vector4>(value);
		}

		[Ordinal(13)] 
		[RED("startFromDefaultValue")] 
		public CBool StartFromDefaultValue
		{
			get => GetPropertyValue<CBool>();
			set => SetPropertyValue<CBool>(value);
		}

		[Ordinal(14)] 
		[RED("defaultInitialValue")] 
		public Vector4 DefaultInitialValue
		{
			get => GetPropertyValue<Vector4>();
			set => SetPropertyValue<Vector4>(value);
		}

		[Ordinal(15)] 
		[RED("inputNode")] 
		public animVectorLink InputNode
		{
			get => GetPropertyValue<animVectorLink>();
			set => SetPropertyValue<animVectorLink>(value);
		}

		[Ordinal(16)] 
		[RED("increaseSpeedNode")] 
		public animVectorLink IncreaseSpeedNode
		{
			get => GetPropertyValue<animVectorLink>();
			set => SetPropertyValue<animVectorLink>(value);
		}

		[Ordinal(17)] 
		[RED("decreaseSpeedNode")] 
		public animVectorLink DecreaseSpeedNode
		{
			get => GetPropertyValue<animVectorLink>();
			set => SetPropertyValue<animVectorLink>(value);
		}

		public animAnimNode_DampVector()
		{
			Id = 4294967295;
			DefaultIncreaseSpeed = new() { X = 1.000000F, Y = 1.000000F, Z = 1.000000F, W = 1.000000F };
			DefaultDecreaseSpeed = new() { X = 1.000000F, Y = 1.000000F, Z = 1.000000F, W = 1.000000F };
			DefaultInitialValue = new();
			InputNode = new();
			IncreaseSpeedNode = new();
			DecreaseSpeedNode = new();
		}
	}
}
