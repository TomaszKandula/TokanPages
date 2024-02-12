import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../../Theme";

export const ApplicationUserInfoStyle = makeStyles(() => ({
    fullname: {
        fontSize: "1.5rem",
        color: Colours.colours.black,
    },
    item: {
        fontSize: "1.1rem",
        color: Colours.colours.black,
    },
    value: {
        fontSize: "1.1rem",
        color: Colours.colours.gray2,
    },
    button: {
        "&:hover": {
            color: Colours.colours.white,
            background: Colours.colours.darkViolet1,
        },
        color: Colours.colours.white,
        background: Colours.colours.violet,
    },
}));
