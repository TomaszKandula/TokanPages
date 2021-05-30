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
        backgroundColor: CustomColours.background.gray1,
        color: CustomColours.typography.white,
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
            backgroundColor: CustomColours.background.lightGray2,
        },
    },
}),
)(TableRow);

export 
{ 
    CustomTableRow, 
    CustomTableCell
};
