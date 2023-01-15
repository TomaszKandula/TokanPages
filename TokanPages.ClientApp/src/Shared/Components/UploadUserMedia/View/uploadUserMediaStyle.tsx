import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../Theme";

export const UploadUserMediaStyle = makeStyles(() => (
{
    button_upload:
    {
        "&:hover": 
        {
            color: Colours.colours.white,
            background: Colours.application.navigation,
        },
        color: Colours.colours.white,
        background: Colours.colours.lightViolet
    }
}));
