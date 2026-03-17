using VContainer;
using VContainer.Unity;
using Calculator.Domain;
using Calculator.Data;
using Calculator.Presentation;
using MessageBox;

public class AppBootstrapper : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        var uiFactory = new UiFactory();
        uiFactory.Build();

        builder.RegisterInstance<ICalculatorView>(uiFactory.CalculatorView);
        builder.RegisterInstance<IMessageBoxView>(uiFactory.MessageBoxView);
        builder.Register<ICalculator, AdditionCalculator>(Lifetime.Singleton);
        builder.Register<ICalculatorRepository, CalculatorRepository>(Lifetime.Singleton);
        builder.Register<MessageBoxPresenter>(Lifetime.Singleton);
        builder.Register<IMessageBoxService, MessageBoxService>(Lifetime.Singleton);
        builder.Register<CalculatorPresenter>(Lifetime.Singleton);

        builder.RegisterBuildCallback(resolver =>
        {
            var presenter = resolver.Resolve<CalculatorPresenter>();
            uiFactory.InputField.onValueChanged.AddListener(presenter.OnInputChanged);
        });
    }
}
