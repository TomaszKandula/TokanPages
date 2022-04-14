import * as React from "react";
import { IGetClientsContent } from "../../Redux/States/Content/getClientsContentState";
import ClientsStyle from "./clientsStyle";

const ClientsView = (props: IGetClientsContent): JSX.Element => 
{
    const classes = ClientsStyle();

    console.log(props);

    return(
        <div className={classes.section}>
        </div>
    );
}

export default ClientsView;
