import { makeStyles } from "@material-ui/core/styles";
import { CustomColours } from "../../Theme/customColours";

const featuredStyle = makeStyles(() => (
{
    section:
    {
        backgroundColor: CustomColours.background.lightGray1
    },
    media: 
    {
        height: "256px"
    }
}));

export default featuredStyle;
