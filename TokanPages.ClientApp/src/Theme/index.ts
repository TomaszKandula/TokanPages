import { createTheme } from "@material-ui/core/styles";

export const AppTheme = createTheme({
    palette: {
        background: {
            default: "#FFFFFF",
        },
    },
    typography: {
        fontFamily: ["Ubuntu", "sans-serif"].join(","),
        fontSize: 14,
        fontWeightLight: 300,
        fontWeightRegular: 400,
        fontWeightMedium: 500,
    },
});

export const AppColours = {
    white: "#FFFFFF",
    ghostWhite: "#F8F8FF",
    violet: "#6367EF",
    lightViolet: "#EBECFB",
    darkViolet1: "#4649C3",
    darkViolet2: "#171E33",
    lightGray1: "#EFEFEF",
    lightGray2: "#FAFAFA",
    lightGray3: "#FCFCFC",
    gray1: "#9E9E9E",
    gray2: "#757575",
    gray3: "#616161",
    gray4: "#212121",
    black: "#000000",
    red: "#f44336",
    redDark: "#e53935",
}
