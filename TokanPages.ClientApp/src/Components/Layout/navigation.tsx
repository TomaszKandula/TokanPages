import * as React from "react";
import { useSelector } from "react-redux";
import { IApplicationState } from "Redux/applicationState";
import Validate from "validate.js";
import { IGetNavigationContent } from "../../Redux/States/getNavigationContentState";
import NavigationView from "./navigationView";
import { 
    ANONYMOUS_NAME, 
    AVATARS_PATH, 
    DEFAULT_AVATAR, 
    DEFAULT_NAME 
} from "../../Shared/constants";

export default function Navigation(props: IGetNavigationContent) 
{
    const user = useSelector((state: IApplicationState) => state.updateUserData);

    const [drawer, setDrawer] = React.useState({ open: false});
    const toggleDrawer = (open: boolean) => (event: any) => 
    {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    let userName = DEFAULT_NAME;
    let avatar = `${AVATARS_PATH}${DEFAULT_AVATAR}`;
    let isAnonymous = true;

    if (!Validate.isEmpty(user?.userData?.userId))
    {
        userName = user?.userData?.aliasName;
        avatar = `${AVATARS_PATH}${user?.userData?.avatarName}`;
        isAnonymous = false;
    }

    return (<NavigationView bind=
    {{  
        drawerState: drawer,
        openHandler: toggleDrawer(true),
        closeHandler: toggleDrawer(false),
        isAnonymous: isAnonymous,
        anonymousText: ANONYMOUS_NAME,
        userAliasText: userName,
        logo: props.content?.logo,
        avatar: avatar,
        menu: props.content?.menu
    }}/>);
}
