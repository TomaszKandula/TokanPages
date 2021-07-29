import * as React from "react";
import ListItem, { ListItemProps } from "@material-ui/core/ListItem";

const CustomListItem = (props: ListItemProps<'a', { button?: true }>): JSX.Element => 
{
    return <ListItem button component="a" {...props} />;
}

export default CustomListItem
