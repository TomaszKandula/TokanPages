import * as React from "react";
import Paper from "@material-ui/core/Paper";
import { Table, TableBody, TableContainer, TableHead, TableRow } from "@material-ui/core";
import { RowItem, TextItem } from "../../Models/TextModel";
import { CustomTableCell, CustomTableRow } from "../../CustomTable/customTable";

export const RenderTable = (props: TextItem): React.ReactElement => {
    const tableData: RowItem[] = props.value as RowItem[];

    const renderHeader = () => {
        let renderBuffer: React.ReactElement[] = [];
        tableData.forEach(item => {
            if (item.column0 === "") {
                renderBuffer.push(
                    <TableRow key={item.column0}>
                        <CustomTableCell>{item.column0}</CustomTableCell>
                        <CustomTableCell>{item.column1}</CustomTableCell>
                        <CustomTableCell>{item.column2}</CustomTableCell>
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
                    <CustomTableRow key={item.column0}>
                        <CustomTableCell component="th" scope="row" className="render-table-header">
                            {item.column0}
                        </CustomTableCell>
                        <CustomTableCell component="td" scope="row" className="render-table-row">
                            {item.column1}
                        </CustomTableCell>
                        <CustomTableCell component="td" scope="row" className="render-table-row">
                            {item.column2}
                        </CustomTableCell>
                    </CustomTableRow>
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
