import * as React from "react";
import { Checkbox, CheckboxProps } from "@material-ui/core";
import { withStyles } from "@material-ui/core/styles";
import { Colours } from "../Colours";

export const VioletCheckbox = withStyles({
    root: {
        color: Colours.colours.violet,
        "&$checked": {
            color: Colours.colours.violet,
        },
    },
    checked: {},
})((props: CheckboxProps) => <Checkbox color="default" {...props} />);
