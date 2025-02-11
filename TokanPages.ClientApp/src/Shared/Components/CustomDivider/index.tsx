import * as React from "react";
import { Divider } from "@material-ui/core";

interface CustomDividerProps {
    mt?: number;
    mb?: number;
}

export const CustomDivider = (props: CustomDividerProps): React.ReactElement => {
    return (
        <div className={`mt-${props.mt ?? 0} mb-${props.mb ?? 0}`}>
            <Divider className="divider" />
        </div>
    );
};
