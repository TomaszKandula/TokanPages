import * as React from "react";
import Paper from "@material-ui/core/Paper";
import { Table, TableBody, TableContainer, TableHead, TableRow, TableCell } from "@material-ui/core";
import { RowItem, TextItem } from "../../Models/TextModel";

export const RenderTable = (props: TextItem): React.ReactElement => {
    const tableData: RowItem[] = props.value as RowItem[];

    const renderHeader = () => {
        let renderBuffer: React.ReactElement[] = [];
        tableData.forEach(item => {
            if (item.column0 === "") {
                renderBuffer.push(
                    <TableRow key={item.column0}>
                        <TableCell className="render-table-head">{item.column0}</TableCell>
                        <TableCell className="render-table-head">{item.column1}</TableCell>
                        <TableCell className="render-table-head">{item.column2}</TableCell>
                    </TableRow>
                );
            }
        });

        return <>{renderBuffer}</>;
    };

    const renderRows = () => {
        let renderBuffer: React.ReactElement[] = [];
        tableData.forEach(item => {
            if (item.column0 !== "") {
                renderBuffer.push(
                    <TableRow key={item.column0} className="render-table-row">
                        <TableCell component="th" scope="row" className="render-table-header">
                            {item.column0}
                        </TableCell>
                        <TableCell component="td" scope="row" className="render-table-cell">
                            {item.column1}
                        </TableCell>
                        <TableCell component="td" scope="row" className="render-table-cell">
                            {item.column2}
                        </TableCell>
                    </TableRow>
                );
            }
        });

        return <>{renderBuffer}</>;
    };

    return (
        <TableContainer component={Paper}>
            <Table className="render-table" aria-label="table">
                <TableHead>{renderHeader()}</TableHead>
                <TableBody>{renderRows()}</TableBody>
            </Table>
        </TableContainer>
    );
};
