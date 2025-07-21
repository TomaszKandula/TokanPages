import * as React from "react";
import { RowItem, TextItem } from "../../Models/TextModel";

export const RenderTable = (props: TextItem): React.ReactElement => {
    const tableData: RowItem[] = props.value as RowItem[];

    const renderHeader = () => {
        let renderBuffer: React.ReactElement[] = [];
        tableData.forEach(item => {
            if (item.column0 === "") {
                renderBuffer.push(
                    <tr key={item.column0}>
                        <td className="is-size-6 has-text-weight-bold">{item.column0}</td>
                        <td className="is-size-6 has-text-weight-bold">{item.column1}</td>
                        <td className="is-size-6 has-text-weight-bold">{item.column2}</td>
                    </tr>
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
                    <tr key={item.column0}>
                        <td className="is-size-6">{item.column0}</td>
                        <td className="is-size-6">{item.column1}</td>
                        <td className="is-size-6">{item.column2}</td>
                    </tr>
                );
            }
        });

        return <>{renderBuffer}</>;
    };

    return (
        <table className="bulma-table bulma-is-striped bulma-is-narrow bulma-is-hoverable bulma-is-fullwidth my-6">
            <thead>{renderHeader()}</thead>
            <tbody>{renderRows()}</tbody>
        </table>
    );
};
