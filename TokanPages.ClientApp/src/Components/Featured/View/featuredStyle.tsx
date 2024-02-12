import { makeStyles } from "@material-ui/core/styles";
import { Colours } from "../../../Theme";

export const FeaturedStyle = makeStyles(() => ({
    section: {
        backgroundColor: Colours.colours.lightGray3,
    },
    caption_text: {
        textAlign: "center",
        fontSize: "2.0rem",
        color: Colours.colours.darkViolet1,
    },
    card: {
        borderRadius: 15,
        boxShadow: "0 2px 20px 0 rgb(0 0 0 / 20%)",
    },
    card_content: {
        minHeight: 150,
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        justifyContent: "center",
    },
    card_title: {
        fontSize: "1.5rem",
        fontWeight: 700,
        color: Colours.colours.black,
    },
    card_subtitle: {
        fontSize: "1.0rem",
        fontWeight: 400,
        color: Colours.colours.gray1,
    },
    card_media: {
        height: 256,
    },
}));
