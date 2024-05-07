import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../Theme";

export const BackArrowStyle = makeStyles(() => ({
    icon: {
        border: `solid 1px ${Colours.colours.lightGray2}`,
    },
    divider: {
        marginTop: 20,
    },
}));
