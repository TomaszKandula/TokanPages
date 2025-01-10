import * as React from "react";
import { Divider } from "@material-ui/core";

interface CustomDividerProps {
    marginTop?: number;
    marginBottom?: number;
}

export const CustomDivider = (props: CustomDividerProps): React.ReactElement => {
    return (
        <div style={{ marginTop: props.marginTop ?? 0, marginBottom: props.marginBottom ?? 0 }}>
            <Divider className="divider" />
        </div>
    );
};
