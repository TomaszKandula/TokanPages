import * as React from "react";
import { Checkbox, CheckboxProps } from "@material-ui/core";
import { withStyles } from "@material-ui/core/styles";
import { CustomColours } from "../Theme/customColours";

const VioletCheckbox = withStyles(
{
    root: 
    {
        color: CustomColours.colours.violet,
        "&$checked":
        {
            color: CustomColours.colours.violet,
        },
    },
    checked: {},
})((props: CheckboxProps) => <Checkbox color="default" {...props} />);

export default VioletCheckbox;
