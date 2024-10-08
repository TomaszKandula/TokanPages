import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { GET_USER_IMAGE } from "../../../Api/Request";
import { ApplicationState } from "../../../Store/Configuration";
import { LanguageChangeEvent } from "../../../Shared/types";
import { SetDataInStorage } from "../../../Shared/Services/StorageServices";
import { SELECTED_LANGUAGE } from "../../../Shared/constants";
import { NavigationView } from "./View/navigationView";
import Validate from "validate.js";

import { UserDataStoreAction, ApplicationLanguageAction } from "../../../Store/Actions";

interface NavigationProps {
    backNavigationOnly?: boolean;
    backPathFragment?: string;
}

export const Navigation = (props: NavigationProps): React.ReactElement => {
    const dispatch = useDispatch();

    const store = useSelector((state: ApplicationState) => state.userDataStore);
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const contentPageData = useSelector((state: ApplicationState) => state.contentPageData);
    const isLoading = contentPageData.isLoading;
    const navigation = contentPageData.components.navigation;

    const avatarSource = GET_USER_IMAGE.replace("{id}", store?.userData?.userId).replace(
        "{name}",
        store?.userData?.avatarName
    );
    const isAnonymous = Validate.isEmpty(store?.userData?.userId);
    const [drawer, setDrawer] = React.useState({ open: false });

    const toggleDrawer = (open: boolean) => (event: any) => {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) return;
        setDrawer({ ...drawer, open });
    };

    const languageHandler = React.useCallback(
        (event: LanguageChangeEvent) => {
            const value = event.target.value as string;
            SetDataInStorage({ selection: value, key: SELECTED_LANGUAGE });
            dispatch(ApplicationLanguageAction.set({ id: value, languages: language.languages }));
        },
        [language?.languages]
    );

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
            userAliasText={store?.userData?.aliasName}
            avatarName={store?.userData?.avatarName}
            avatarSource={avatarSource}
            logoImgName={navigation?.logo}
            languages={language}
            languageId={language?.id}
            languageHandler={languageHandler}
            menu={navigation?.menu}
            backNavigationOnly={props.backNavigationOnly}
            backPathFragment={props.backPathFragment}
        />
    );
};
