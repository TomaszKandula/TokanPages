import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { GET_USER_IMAGE } from "../../../Api";
import { UserDataStoreAction, ApplicationLanguageAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";
import { NavigationView } from "./View/navigationView";
import Validate from "validate.js";

interface NavigationProps {
    backNavigationOnly?: boolean;
    backPathFragment?: string;
}

export const Navigation = (props: NavigationProps): React.ReactElement => {
    const dispatch = useDispatch();

    const data = useSelector((state: ApplicationState) => state.contentPageData);
    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);

    const isLoading = data?.isLoading;
    const navigation = data?.components.layoutNavigation;
    const userId = store?.userData?.userId;
    const aliasName = store?.userData?.aliasName;
    const avatarName = store?.userData?.avatarName;
    const isAnonymous = Validate.isEmpty(userId);

    const avatarSource = GET_USER_IMAGE.replace("{id}", userId).replace("{name}", avatarName);

    const [drawer, setDrawer] = React.useState({ open: false });
    const [isLanguageMenuOpen, setIsLanguageMenuOpen] = React.useState(false);

    const toggleDrawer = (open: boolean) => (event: any) => {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    const languagePickHandler = React.useCallback(
        (id: string) => {
            const pathname = window.location.pathname;
            const paths = pathname.split("/").filter(e => String(e).trim());
            const newUrl = window.location.href.replace(`/${paths[0]}`, `/${id}`);

            window.history.pushState({}, "", newUrl);
            dispatch(
                ApplicationLanguageAction.set({
                    ...language,
                    id: id,
                    languages: language.languages,
                })
            );
        },
        [language]
    );

    const languageMenuHandler = React.useCallback(() => {
        setIsLanguageMenuOpen(!isLanguageMenuOpen);
    }, [isLanguageMenuOpen]);

    const onAvatarClick = React.useCallback(() => {
        if (isAnonymous) return;
        dispatch(UserDataStoreAction.show(true));
    }, [isAnonymous]);

    return (
        <NavigationView
            isLoading={isLoading}
            drawerState={drawer}
            openHandler={toggleDrawer(true)}
            closeHandler={toggleDrawer(false)}
            infoHandler={onAvatarClick}
            isAnonymous={isAnonymous}
            userAliasText={aliasName}
            avatarName={avatarName}
            avatarSource={avatarSource}
            logoImgName={navigation?.logo}
            menu={navigation?.menu}
            backNavigationOnly={props.backNavigationOnly}
            backPathFragment={props.backPathFragment}
            languages={language}
            languageId={language?.id}
            languagePickHandler={languagePickHandler}
            languageMenuHandler={languageMenuHandler}
            isLanguageMenuOpen={isLanguageMenuOpen}
        />
    );
};
