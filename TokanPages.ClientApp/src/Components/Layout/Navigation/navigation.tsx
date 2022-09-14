import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "Redux/applicationState";
import { IGetNavigationContent } from "../../../Redux/States/Content/getNavigationContentState";
import { ActionCreators as UserDataAction } from "../../../Redux/Actions/Users/storeUserDataAction";
import { ActionCreators as UserLanguageAction } from "../../../Redux/Actions/userLanguageAction";
import { GetLanguages, SetUserLanguageInStore, GetDefaultLanguageId } from "../../../Shared/Services/languageService";
import { NavigationView } from "./View/navigationView";
import Validate from "validate.js";

export const Navigation = (props: IGetNavigationContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const user = useSelector((state: IApplicationState) => state.storeUserData);
    const languages = GetLanguages();
    const defaultLanguage = GetDefaultLanguageId();
    const isAnonymous = Validate.isEmpty(user?.userData?.userId);

    const [drawer, setDrawer] = React.useState({ open: false});
    const [language, setLanguage] = React.useState(defaultLanguage);

    const languageHandler = (event: React.ChangeEvent<{ value: unknown }>) => 
    {
        const value = event.target.value as string;
        setLanguage(value);
        SetUserLanguageInStore(value);
        dispatch(UserLanguageAction.setAnotherLanguage(value));
    };

    const toggleDrawer = (open: boolean) => (event: any) => 
    {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    const onAvatarClick = () => 
    {
        if (isAnonymous) return;
        dispatch(UserDataAction.show(true));
    }

    return (<NavigationView bind=
    {{  
        isLoading: props.isLoading,
        drawerState: drawer,
        openHandler: toggleDrawer(true),
        closeHandler: toggleDrawer(false),
        infoHandler: onAvatarClick,
        isAnonymous: isAnonymous,
        userAliasText: user?.userData?.aliasName,
        logo: props.content?.logo,
        avatarName: user?.userData?.avatarName,
        languages: languages,
        selectedLanguage: language,
        languageHandler: languageHandler,
        menu: props.content?.menu
    }}/>);
}
