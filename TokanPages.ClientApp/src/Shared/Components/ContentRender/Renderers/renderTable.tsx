import * as React from "react";
import Paper from "@material-ui/core/Paper";
import { Table, TableBody, TableContainer, TableHead, TableRow } from "@material-ui/core";
import { IRowItem, ITextItem } from "../Models/textModel";
import { CustomTableCell, CustomTableRow } from "../CustomTable/customTable";
import renderTableStyle from "../Styles/renderTableStyle";

export function RenderTable(props: ITextItem)
{
    const tableData: IRowItem[] = props.value as IRowItem[];
    const classes = renderTableStyle();

    const renderHeader = () =>
    {
        let renderBuffer: JSX.Element[] = [];
        tableData.forEach(item =>
        {
            if (item.column0 === "") 
            {
                renderBuffer.push(
                    <TableRow key={item.column0}>
                        <CustomTableCell>{item.column0}</CustomTableCell>
                        <CustomTableCell>{item.column1}</CustomTableCell>
                        <CustomTableCell>{item.column2}</CustomTableCell>
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
                    <CustomTableRow key={item.column0}>
                        <CustomTableCell component="th" scope="row" className={classes.header}>{item.column0}</CustomTableCell>
                        <CustomTableCell component="td" scope="row" className={classes.row}>{item.column1}</CustomTableCell>
                        <CustomTableCell component="td" scope="row" className={classes.row}>{item.column2}</CustomTableCell>
                    </CustomTableRow>
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
