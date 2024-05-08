import { makeStyles } from "@material-ui/core";
import { Colours } from "../../../../Theme";

export const ArticleDetailStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.white,
    },
    container: {
        maxWidth: 700,
    },
    icon: {
        border: `solid 1px ${Colours.colours.lightGray2}`,
    },
    dividerTop: {
        marginTop: 20,
        marginBottom: 33,
    },
    dividerBottom: {
        marginTop: 30,
        marginBottom: 30,
    },
    text_block: {
        display: "flex",
        justifyContent: "left",
        flexDirection: "row",
    },
    text_padding_left: {
        paddingLeft: 5,
    },
    flag_image: {
        height: 18,
        width: 18,
        display: "flex",
        alignSelf: "center",
    },
    grid_read_count: {
        display: "flex",
        alignSelf: "center",
        justifyContent: "flex-end",
    },
    text_read_count: {
        display: "flex",
        justifyContent: "flex-end",
    },
    aliasName: {
        paddingTop: 10,
    },
    avatarSmall: {
        color: Colours.colours.white,
        width: 48,
        height: 48,
    },
    avatarLarge: {
        color: Colours.colours.white,
        width: 96,
        height: 96,
    },
    thumbsMedium: {
        color: Colours.colours.gray1,
        cursor: "pointer",
        width: 24,
        height: 24,
    },
    likesTip: {
        fontSize: "1.5em",
    },
    popover: {
        pointerEvents: "none",
    },
}));
