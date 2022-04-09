import { CustomColours } from "../../../../Theme/customColours";
import { 
    createStyles, 
    withStyles, 
    TableCell, 
    TableRow
} from "@material-ui/core";

const CustomTableCell = withStyles(() => createStyles(
{
    head: 
    {
        backgroundColor: CustomColours.colours.gray1,
        color: CustomColours.colours.white,
        fontSize: 18,
        fontWeight: "bold"
    },
    body: 
    {
        fontSize: 15,
    },
}),
)(TableCell);

const CustomTableRow = withStyles(() => createStyles(
{
    root: 
    {
        '&:nth-of-type(odd)': 
        {
            backgroundColor: CustomColours.colours.lightGray2,
        },
    },
}),
)(TableRow);

export 
{ 
    CustomTableRow, 
    CustomTableCell
};
