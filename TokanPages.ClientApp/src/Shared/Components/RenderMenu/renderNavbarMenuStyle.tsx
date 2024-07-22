import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const RenderNavbarMenuStyle = makeStyles((theme) => ({
    list: {
        display: "flex",
        flexDirection: "row",
        flexWrap: "nowrap",
        justifyContent: "space-between",
    },
    list_item_pipe: {
        [theme.breakpoints.down(1100)]: {
            marginTop: 12,
            marginBottom: 12,
            },
        [theme.breakpoints.up(1100)]: {
            marginTop: 10,
            marginBottom: 10,
        },
        display: "flex",
        width: 1.5,
        marginLeft: 2,
        marginRight: 2,
        cursor: "default",
        backgroundColor: Colours.colours.black,
    },
}));
