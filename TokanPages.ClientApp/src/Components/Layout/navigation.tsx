import * as React from "react";
import { IGetNavigationContent } from "../../Redux/States/getNavigationContentState";
import NavigationView from "./navigationView";

export default function Navigation(props: IGetNavigationContent) 
{
    
    const [drawer, setDrawer] = React.useState({ open: false});
    const toggleDrawer = (open: boolean) => (event: any) => 
    {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    // TODO: use user service provider
    const userName = "Dummy";
    const anonymousName = "Anonymous";
    const isAnonymous = true;
    const avatar = "https://maindbstorage.blob.core.windows.net/tokanpages-test/content/avatars/avatar-default-288.jpeg";

    return (<NavigationView bind=
    {{  
        drawerState: drawer,
        openHandler: toggleDrawer(true),
        closeHandler: toggleDrawer(false),
        isAnonymous: isAnonymous,
        anonymousText: anonymousName,
        userAliasText: userName,
        logo: props.content?.logo,
        avatar: avatar,
        menu: props.content?.menu
    }}/>);
}
