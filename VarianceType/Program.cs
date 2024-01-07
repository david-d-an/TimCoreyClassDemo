using VarianceType.Models;


// Contravariant
// DContravariant<Control> dContraControl = ContravariantControl;
// DContravariant<Button> dContraButton = ContravariantButton;
// dContraButton = dContraControl;
DContravariant<Button> dContraButton = ContravariantControl;
dContraButton(new Button());

// Covariant
// DCovariant<Control> dCoControl = CovariantControl;
// DCovariant<Button> dCoButton = CovariantButton;
// dCoControl = dCoButton;
DCovariant<Control> dCoControl = CovariantButton;
Control c = dCoControl();
return;

// in: Contra
// out: Co

// Methods that match the delegate signature.
void ContravariantControl(Control argument)
{ }
void ContravariantButton(Button argument)
{ }

Control CovariantControl() => new Control();

Button CovariantButton() => new Button();

// Contravariant delegate.
public delegate void DContravariant<in T>(T argument);
public delegate T DCovariant<out T>();