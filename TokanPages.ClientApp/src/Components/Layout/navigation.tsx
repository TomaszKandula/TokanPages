import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { IApplicationState } from "Redux/applicationState";
import Validate from "validate.js";
import { IGetNavigationContent } from "../../Redux/States/Content/getNavigationContentState";
import { ActionCreators } from "../../Redux/Actions/Users/storeUserDataAction";
import { GetLanguages, SetUserLanguage, GetDefaultLanguageId } from "../../Shared/Services/languageService";
import NavigationView from "./navigationView";
import Reload from "./Services/componentsContentService";
import { 
    ANONYMOUS_NAME, 
    AVATARS_PATH, 
    DEFAULT_AVATAR, 
    DEFAULT_NAME 
} from "../../Shared/constants";

const Navigation = (props: IGetNavigationContent): JSX.Element => 
{
    const dispatch = useDispatch();
    const user = useSelector((state: IApplicationState) => state.storeUserData);
    const languages = GetLanguages();
    const defaultLanguage = GetDefaultLanguageId();

    const [drawer, setDrawer] = React.useState({ open: false});
    const [language, setLanguage] = React.useState(defaultLanguage);

    const languageHandler = (event: React.ChangeEvent<{ value: unknown }>) => 
    {
        const value = event.target.value as string;
        setLanguage(value);
        SetUserLanguage(value);
        Reload(dispatch);
    };

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

    const onAvatarClick = () => 
    {
        if (isAnonymous) return;
        dispatch(ActionCreators.show(true));
    }

    return (<NavigationView bind=
    {{  
        isLoading: props.isLoading,
        drawerState: drawer,
        openHandler: toggleDrawer(true),
        closeHandler: toggleDrawer(false),
        infoHandler: onAvatarClick,
        isAnonymous: isAnonymous,
        anonymousText: ANONYMOUS_NAME,
        userAliasText: userName,
        logo: props.content?.logo,
        avatar: avatar,
        languages: languages,
        selectedLanguage: language,
        languageHandler: languageHandler,
        menu: props.content?.menu
    }}/>);
}

export default Navigation;
