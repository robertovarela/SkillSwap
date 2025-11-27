using MudBlazor;

namespace RDS.Server.Utils;

public static class ConfigurationTheme
{
    /*
!
 * Bootswatch v5.3.3 (https://bootswatch.com)
 * Theme: sandstone
 * Copyright 2012-2024 Thomas Park
 * Licensed under MIT
 * Based on Bootstrap
    */
    public static readonly MudTheme Theme = new()
    {
        PaletteLight = new PaletteLight()
        {
            Black = "#000",
            White = "#fff",
            Primary = "#FF9F45",
            PrimaryContrastText = "#d6dfe7",
            Secondary = "#8e8c84",
            SecondaryContrastText = "#e8e8e6",
            Tertiary = "#c9ae74ff",
            TertiaryContrastText = "#f8f9fa",
            Info = "#29abe0",
            InfoContrastText = "#d4eef9",
            Success = "#93c54b",
            SuccessContrastText = "#e9f3db",
            Warning = "#f47c3c",
            WarningContrastText = "#fde5d8",
            Error = "rgba(244,67,54,1)",
            ErrorContrastText = "rgba(255,255,255,1)",
            Dark = "#3e3f3a",
            DarkContrastText = "#ced4da",
            TextPrimary = "#142536",
            TextSecondary = "#393835",
            TextDisabled = "rgba(0,0,0,0.3764705882352941)",
            ActionDefault = "rgba(0,0,0,0.5372549019607843)",
            ActionDisabled = "rgba(0,0,0,0.25882352941176473)",
            ActionDisabledBackground = "rgba(0,0,0,0.11764705882352941)",
            Background = "#f9f0deff", //rgba(245,245,245,1)
            BackgroundGray = "#fbd198ff",
            Surface = "#f9f0deff",
            DrawerBackground = "#f8e4b5d4",
            DrawerText = "#414141", //"#8e8c84",
            DrawerIcon = "#8e8c84",
            AppbarBackground = "#ffb31fff",
            AppbarText = "#325d88",
            LinesDefault = "rgba(0,0,0,0.11764705882352941)",
            LinesInputs = "rgba(189,189,189,1)",
            TableLines = "rgba(224,224,224,1)",
            TableStriped = "#fcdcb9",
            TableHover = "#fff4e6",
            Divider = "rgba(224,224,224,1)",
            DividerLight = "rgba(0,0,0,0.8)",
            PrimaryDarken = "#142536",
            PrimaryLighten = "#d6dfe7",
            SecondaryDarken = "#393835",
            SecondaryLighten = "#e8e8e6",
            TertiaryDarken = "#705825ff",
            TertiaryLighten = "#f3daa4ff",
            InfoDarken = "#10445a",
            InfoLighten = "#d4eef9",
            SuccessDarken = "#3b4f1e",
            SuccessLighten = "#e9f3db",
            WarningDarken = "#623218",
            WarningLighten = "#fde5d8",
            ErrorDarken = "rgb(242,28,13)",
            ErrorLighten = "rgb(246,96,85)",
            DarkDarken = "rgb(46,46,46)",
            DarkLighten = "rgb(87,87,87)",
            HoverOpacity = 0.06,
            RippleOpacity = 0.1,
            RippleOpacitySecondary = 0.2,
            GrayDefault = "#9E9E9E",
            GrayLight = "#BDBDBD",
            GrayLighter = "#E0E0E0",
            GrayDark = "#757575",
            GrayDarker = "#616161",
            OverlayDark = "rgba(33,33,33,0.4980392156862745)",
            OverlayLight = "rgba(255,255,255,0.4980392156862745)",
        },
        PaletteDark = new PaletteDark()
        {
            Black = "#000",
            White = "#fff",
            Primary = "#325d88",
            PrimaryContrastText = "#e2e4e7ff",
            Secondary = "#8e8c84",
            SecondaryContrastText = "#e2e4e7ff",
            Tertiary = "#325d88",
            TertiaryContrastText = "#e2e4e7ff",
            Info = "#29abe0",
            InfoContrastText = "#08222d",
            Success = "#93c54b",
            SuccessContrastText = "#1d270f",
            Warning = "#f47c3c",
            WarningContrastText = "#31190c",
            Error = "rgba(244,67,54,1)",
            ErrorContrastText = "rgba(255,255,255,1)",
            Dark = "#3e3f3a",
            DarkContrastText = "#1f201d",
            TextPrimary = "#849eb8",
            TextSecondary = "#bbbab5",
            TextDisabled = "rgba(255,255,255,0.2)",
            ActionDefault = "rgba(173,173,177,1)",
            ActionDisabled = "rgba(255,255,255,0.25882352941176473)",
            ActionDisabledBackground = "rgba(255,255,255,0.11764705882352941)",
            Background = "rgba(50,51,61,1)",
            BackgroundGray = "rgba(39,39,47,1)",
            Surface = "rgba(55,55,64,1)",
            DrawerBackground = "#1c1c1a",
            DrawerText = "#8e8c84",
            DrawerIcon = "#8e8c84",
            AppbarBackground = "#0a131b",
            AppbarText = "#325d88",
            LinesDefault = "rgba(255,255,255,0.11764705882352941)",
            LinesInputs = "rgba(255,255,255,0.2980392156862745)",
            TableLines = "rgba(255,255,255,0.11764705882352941)",
            TableStriped = "rgba(255,255,255,0.2)",
            TableHover = "#fcdcb9",
            Divider = "rgba(255,255,255,0.11764705882352941)",
            DividerLight = "rgba(255,255,255,0.058823529411764705)",
            PrimaryDarken = "#849eb8",
            PrimaryLighten = "#0a131b",
            SecondaryDarken = "#bbbab5",
            SecondaryLighten = "#1c1c1a",
            TertiaryDarken = "#886c30ff",
            TertiaryLighten = "#f7e3b2ff",
            InfoDarken = "#7fcdec",
            InfoLighten = "#08222d",
            SuccessDarken = "#bedc93",
            SuccessLighten = "#1d270f",
            WarningDarken = "#f8b08a",
            WarningLighten = "#31190c",
            ErrorDarken = "rgb(242,28,13)",
            ErrorLighten = "rgb(246,96,85)",
            DarkDarken = "rgb(23,23,28)",
            DarkLighten = "rgb(56,56,67)",
        },
        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "4px",
            DrawerMiniWidthLeft = "56px",
            DrawerMiniWidthRight = "56px",
            DrawerWidthLeft = "240px",
            DrawerWidthRight = "240px",
            AppbarHeight = "64px",
        },
        Typography = new Typography()
        {
            Default = new DefaultTypography
            {
                FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
                FontWeight = "400",
                FontSize = ".875rem",
                LineHeight = "1.43",
                LetterSpacing = ".01071em",
                TextTransform = "none",
            },
            H1 = new H1Typography
            {
                FontWeight = "300",
                FontSize = "6rem",
                LineHeight = "1.167",
                LetterSpacing = "-.01562em",
                TextTransform = "none",
            },
            H2 = new H2Typography
            {
                FontWeight = "300",
                FontSize = "3.75rem",
                LineHeight = "1.2",
                LetterSpacing = "-.00833em",
                TextTransform = "none",
            },
            H3 = new H3Typography
            {
                FontWeight = "400",
                FontSize = "3rem",
                LineHeight = "1.167",
                LetterSpacing = "0",
                TextTransform = "none",
            },
            H4 = new H4Typography
            {
                FontWeight = "400",
                FontSize = "2.125rem",
                LineHeight = "1.235",
                LetterSpacing = ".00735em",
                TextTransform = "none",
            },
            H5 = new H5Typography
            {
                FontWeight = "400",
                FontSize = "1.5rem",
                LineHeight = "1.334",
                LetterSpacing = "0",
                TextTransform = "none",
            },
            H6 = new H6Typography
            {
                FontWeight = "500",
                FontSize = "1.25rem",
                LineHeight = "1.6",
                LetterSpacing = ".0075em",
                TextTransform = "none",
            },
            Subtitle1 = new Subtitle1Typography
            {
                FontWeight = "400",
                FontSize = "1rem",
                LineHeight = "1.75",
                LetterSpacing = ".00938em",
                TextTransform = "none",
            },
            Subtitle2 = new Subtitle2Typography
            {
                FontWeight = "500",
                FontSize = ".875rem",
                LineHeight = "1.57",
                LetterSpacing = ".00714em",
                TextTransform = "none",
            },
            Body1 = new Body1Typography
            {
                FontWeight = "400",
                FontSize = "1rem",
                LineHeight = "1.5",
                LetterSpacing = ".00938em",
                TextTransform = "none",
            },
            Body2 = new Body2Typography
            {
                FontWeight = "400",
                FontSize = ".875rem",
                LineHeight = "1.43",
                LetterSpacing = ".01071em",
                TextTransform = "none",
            },
            Button = new ButtonTypography
            {
                FontWeight = "500",
                FontSize = ".875rem",
                LineHeight = "1.75",
                LetterSpacing = ".02857em",
                TextTransform = "uppercase",
            },
            Caption = new CaptionTypography
            {
                FontWeight = "400",
                FontSize = ".75rem",
                LineHeight = "1.66",
                LetterSpacing = ".03333em",
                TextTransform = "none",
            },
            Overline = new OverlineTypography
            {
                FontWeight = "400",
                FontSize = ".75rem",
                LineHeight = "2.66",
                LetterSpacing = ".08333em",
                TextTransform = "none",
            },
        },
        ZIndex = new ZIndex()
        {
            Drawer = 1100,
            Popover = 1200,
            AppBar = 1300,
            Dialog = 1400,
            Snackbar = 1500,
            Tooltip = 1600,
        },
    };



    public static MudTheme Theme2 => new()
    {
        Typography = new Typography
        {
            Default = new DefaultTypography()
            {
                FontFamily = ["Karla", "Poppins", "Roboto", "sans-serif"],
            }
        },
        PaletteLight = DefaultLightPalette,
        PaletteDark = DefaultDarkPalette,
    };

    private static readonly PaletteLight DefaultLightPalette = new()
    {
        Primary = "#FF9F45", // Cor primária do tema
        Surface = "#f9f9f9", // Cor da superfície dos componentes
        Background = "#ffffff", // Cor de fundo geral
        BackgroundGray = "#e0e0e0", // Cor de fundo cinza
        TextPrimary = "#000000", // Cor primária do texto
        TextSecondary = "#424242", // Cor secundária do texto
        TextDisabled = "#00000080", // Cor do texto desabilitado
        DrawerIcon = "#424242", // Cor dos ícones do drawer
        DrawerText = "#424242", // Cor do texto do drawer
        DrawerBackground = "#ffffff", // Cor de fundo do drawer
        ActionDefault = "#424242", // Cor padrão das ações
        ActionDisabled = "#9999994d", // Cor das ações desabilitadas
        ActionDisabledBackground = "#605f6d4d", // Cor de fundo das ações desabilitadas
        Info = "#4a86ff", // Cor de informação
        Success = "#FF9F45", // Cor de sucesso
        Warning = "#ffb545", // Cor de aviso
        Error = "#ff3f5f", // Cor de erro
        LinesDefault = "#e0e0e0", // Cor padrão das linhas
        TableLines = "#e0e0e0", // Cor das linhas das tabelas
        Divider = "#bdbdbd", // Cor dos divisores
        OverlayLight = "rgba(255,255,255,0.8)", // Cor clara de sobreposição
        AppbarBackground = "#FF9F45", // Cor de fundo da barra de aplicativo
        AppbarText = "#424242", // Cor do texto da barra de aplicativo
    };

    private static readonly PaletteDark DefaultDarkPalette = new()
    {
        Primary = "#7e6fff", // Cor primária do tema
        Surface = "#1e1e2d", // Cor da superfície dos componentes
        Background = "#1a1a27", // Cor de fundo geral
        BackgroundGray = "#151521", // Cor de fundo cinza
        TextPrimary = "#b2b0bf", // Cor primária do texto
        TextSecondary = "#92929f", // Cor secundária do texto
        TextDisabled = "#ffffff33", // Cor do texto desabilitado
        DrawerIcon = "#92929f", // Cor dos ícones do drawer
        DrawerText = "#92929f", // Cor do texto do drawer
        DrawerBackground = "#1a1a27", // Cor de fundo do drawer
        ActionDefault = "#74718e", // Cor padrão das ações
        ActionDisabled = "#9999994d", // Cor das ações desabilitadas
        ActionDisabledBackground = "#605f6d4d", // Cor de fundo das ações desabilitadas
        Info = "#4a86ff", // Cor de informação
        Success = "#3dcb6c", // Cor de sucesso
        Warning = "#ffb545", // Cor de aviso
        Error = "#ff3f5f", // Cor de erro
        LinesDefault = "#33323e", // Cor padrão das linhas
        TableLines = "#33323e", // Cor das linhas das tabelas
        Divider = "#292838", // Cor dos divisores
        OverlayLight = "rgba(30,30,45,0.8)", // Cor clara de sobreposição
        AppbarBackground = "rgba(26,26,39,0.8)", // Cor de fundo da barra de aplicativo
        AppbarText = "#92929f", // Cor do texto da barra de aplicativo
    };
}
/*private static readonly PaletteLight _lightPalette = new()
{
    Black = "#110e2d",
    AppbarText = "#424242",
    AppbarBackground = "rgba(255,255,255,0.8)",
    DrawerBackground = "#ffffff",
    GrayLight = "#e8e8e8",
    GrayLighter = "#f9f9f9",
};

private static readonly PaletteDark _darkPalette = new()
{
    Primary = "#7e6fff",
    Surface = "#1e1e2d",
    Background = "#1a1a27",
    BackgroundGray = "#151521",
    AppbarText = "#92929f",
    AppbarBackground = "rgba(26,26,39,0.8)",
    DrawerBackground = "#1a1a27",
    ActionDefault = "#74718e",
    ActionDisabled = "#9999994d",
    ActionDisabledBackground = "#605f6d4d",
    TextPrimary = "#b2b0bf",
    TextSecondary = "#92929f",
    TextDisabled = "#ffffff33",
    DrawerIcon = "#92929f",
    DrawerText = "#92929f",
    GrayLight = "#2a2833",
    GrayLighter = "#1e1e2d",
    Info = "#4a86ff",
    Success = "#3dcb6c",
    Warning = "#ffb545",
    Error = "#ff3f5f",
    LinesDefault = "#33323e",
    TableLines = "#33323e",
    Divider = "#292838",
    OverlayLight = "#1e1e2d80",
};*/


/*PaletteLight = new PaletteLight
{
    Primary = Colors.Blue.Default,
    Secondary = Colors.Amber.Accent3,
    Tertiary = Colors.Teal.Darken3,
    Background = Colors.Gray.Lighten5,
    Surface = "#1e1e2d",
    AppbarBackground = Colors.Blue.Darken4,
    AppbarText = Colors.Shades.White,
    TextPrimary = Colors.Shades.Black,
    DrawerText = Colors.Shades.Black,
    DrawerBackground = Colors.Gray.Lighten4,
    PrimaryContrastText = Colors.Shades.White
},



PaletteDark = new PaletteDark
{
    //Primary = Colors.Teal.Accent3,
    //Secondary = Colors.LightGreen.Darken3,
    Primary = Colors.Blue.Default,
    Secondary = Colors.Amber.Accent3,
    Tertiary = Colors.Cyan.Darken4,
    Background = Colors.Gray.Darken4,
    Surface = Colors.Amber.Accent1,
    AppbarBackground = Colors.BlueGray.Darken4,
    AppbarText = Colors.Shades.White,
    TextPrimary = Colors.Shades.White,
    DrawerText = Colors.Shades.White,
    DrawerBackground = Colors.BlueGray.Darken3,
    PrimaryContrastText = Colors.Shades.White
}*/

    /*
!
 * Bootswatch v5.3.3 (https://bootswatch.com)
 * Theme: sandstone
 * Copyright 2012-2024 Thomas Park
 * Licensed under MIT
 * Based on Bootstrap
    */
