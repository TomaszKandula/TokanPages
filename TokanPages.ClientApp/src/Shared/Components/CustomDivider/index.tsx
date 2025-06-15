import * as React from "react";

interface CustomDividerProps {
    mt?: number;
    mb?: number;
}
//TODO: delete this component
export const CustomDivider = (props: CustomDividerProps): React.ReactElement => (<hr className={`mt-${props.mt ?? 0} mb-${props.mb ?? 0}`} />);
