import * as React from "react";
import Paper from "@material-ui/core/Paper";
import { Table, TableBody, TableContainer, TableHead, TableRow } from "@material-ui/core";
import { useStyles, StyledTableRow, StyledTableCell } from "../Styles/renderTableStyle";
import { IRowItem, ITextItem } from "../Models/textModel";

export function RenderTable(props: ITextItem)
{
    const tableData: IRowItem[] = props.value as IRowItem[];
    const classes = useStyles();

    const renderHeader = () =>
    {
        let renderBuffer: JSX.Element[] = [];
        tableData.forEach(item =>
        {
            if (item.column0 === "") 
            {
                renderBuffer.push(
                    <TableRow key={item.column0}>
                        <StyledTableCell>{item.column0}</StyledTableCell>
                        <StyledTableCell>{item.column1}</StyledTableCell>
                        <StyledTableCell>{item.column2}</StyledTableCell>
                    </TableRow>
                );
            }
        });

        return(<>{renderBuffer}</>);
    };

    const renderRows = () => 
    {
        let renderBuffer: JSX.Element[] = [];
        tableData.forEach(item =>
        {
            if (item.column0 !== "") 
            {
                renderBuffer.push(
                    <StyledTableRow key={item.column0}>
                        <StyledTableCell component="th" scope="row" className={classes.header}>{item.column0}</StyledTableCell>
                        <StyledTableCell component="td" scope="row" className={classes.row}>{item.column1}</StyledTableCell>
                        <StyledTableCell component="td" scope="row" className={classes.row}>{item.column2}</StyledTableCell>
                    </StyledTableRow>
                );
            }    
        });
    
        return(<>{renderBuffer}</>);
    };

    return(
        <TableContainer component={Paper}>
            <Table className={classes.table} aria-label="table">
                <TableHead>
                    {renderHeader()}
                </TableHead>
                <TableBody>
                    {renderRows()}
                </TableBody>
            </Table>
        </TableContainer>    
    );
}
