import * as React from "react";
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { GET_USER_IMAGE } from "../../../Api";
import { UserDataStoreAction, ApplicationLanguageAction } from "../../../Store/Actions";
import { ApplicationState } from "../../../Store/Configuration";
import { useDimensions, useQuery } from "../../../Shared/Hooks";
import { NavigationView } from "./View/navigationView";
import Validate from "validate.js";

interface NavigationProps {
    backNavigationOnly?: boolean;
}

export const Navigation = (props: NavigationProps): React.ReactElement => {
    const media = useDimensions();
    const query = useQuery();
    const dispatch = useDispatch();
    const history = useHistory();

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

    const [isMenuOpen, setIsMenuOpen] = React.useState(false);
    const [isBottomSheetOpen, setIsBottomSheetOpen] = React.useState(false);
    const [isLanguageMenuOpen, setIsLanguageMenuOpen] = React.useState(false);

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

    const triggerSideMenu = React.useCallback(() => {
        setIsMenuOpen(!isMenuOpen);
    }, [isMenuOpen]);

    const triggerBottomSheet = React.useCallback(() => {
        setIsBottomSheetOpen(!isBottomSheetOpen);
    }, [isBottomSheetOpen]);

    const languageMenuHandler = React.useCallback(() => {
        setIsLanguageMenuOpen(!isLanguageMenuOpen);
    }, [isLanguageMenuOpen]);

    const infoHandler = React.useCallback(() => {
        if (isAnonymous) {
            return;
        }

        dispatch(UserDataStoreAction.show(true));
    }, [isAnonymous]);

    const backButtonHandler = React.useCallback(() => {
        const redirect = query.get("redirect") ?? "";
        const backPath = `/${language?.id}/${redirect}`;

        if (Validate.isEmpty(redirect)) {
            history.push(`/${language?.id}`);
        } else {
            history.push(backPath);
        }
    }, [language?.id]);

    return (
        <NavigationView
            isLoading={isLoading}
            isAnonymous={isAnonymous}
            isMenuOpen={isMenuOpen}
            isBottomSheetOpen={isBottomSheetOpen}
            media={media}
            triggerSideMenu={triggerSideMenu}
            triggerBottomSheet={triggerBottomSheet}
            infoHandler={infoHandler}
            aliasName={aliasName}
            avatarName={avatarName}
            avatarSource={avatarSource}
            navigation={navigation}
            backNavigationOnly={props.backNavigationOnly}
            backPathHandler={backButtonHandler}
            languages={language}
            languageId={language?.id}
            languageFlagType={language?.flagImageType}
            languagePickHandler={languagePickHandler}
            languageMenuHandler={languageMenuHandler}
            isLanguageMenuOpen={isLanguageMenuOpen}
        />
    );
};
