
namespace TDA.Controls;

public partial class MySteper : ContentView
{
	public MySteper()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
		propertyName: nameof(Minimum),
		returnType: typeof(int),
		defaultValue: 3,
		defaultBindingMode:BindingMode.OneWay,
		declaringType: typeof(MySteper)
		);
    
    public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
        propertyName: nameof(Maximum),
        returnType: typeof(int),
        defaultValue: 60,
        defaultBindingMode: BindingMode.OneWay,
        declaringType: typeof(MySteper)
        );
    
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(
        propertyName: nameof(Value),
        returnType: typeof(int),
        defaultValue: 10,
        defaultBindingMode: BindingMode.TwoWay,
        declaringType: typeof(MySteper)
        );
    public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
        propertyName: nameof(IsEnabled),
        returnType: typeof(bool),
        defaultValue: true,
        defaultBindingMode: BindingMode.OneWay,
        declaringType: typeof(MySteper)
        );
    //public static readonly BindableProperty TextProperty = BindableProperty.Create(
    //    propertyName: nameof(Text),
    //    returnType: typeof(string),
    //    defaultValue: "0",
    //    defaultBindingMode: BindingMode.OneWay,
    //    declaringType: typeof(MySteper)
    //    );

    public int Minimum
	{
		get { return (int)GetValue(MinimumProperty); }
		set { SetValue(MinimumProperty, value); }
	}
    public int Maximum
    {
        get { return (int)GetValue(MaximumProperty); }
		set { SetValue(MaximumProperty, value); }
    }
    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }
    //public string Text
    //{
    //    get { return (string)GetValue(TextProperty); }
    //    set { SetValue(TextProperty, value); }
    //}
    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }
    private void ButtonPlus_Clicked(object sender, EventArgs e)
    {
        if (Value < Maximum)
        {
           Value ++;
        }
    }

    private void ButtonMinus_Clicked(object sender, EventArgs e)
    {
        if (Value > Minimum) 
        { 
           Value --; 
        }
    }
}