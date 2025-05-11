import * as React from "react";
import { useSelector } from "react-redux";
import { BrowserRouter, Switch } from "react-router-dom";
import { ApplicationState } from "./Store/Configuration";
import { GetContentManifestDto, LanguageItemDto } from "./Api/Models";
import { HasSnapshotMode } from "./Shared/Services/SpaCaching";
import { useAnimation, useApplicationLanguage, useUserData, useXssWarning } from "./Shared/Hooks";
import { MapComponentsToRoutes } from "./routes";
import {
    ClearPageStart,
    ScrollToTop,
    ApplicationCookie,
    ApplicationToast,
    ApplicationDialogBox,
    ApplicationUserInfo,
    ApplicationSession,
} from "./Shared/Components";

interface AppProps {
    manifest: GetContentManifestDto | undefined;
}

interface RenderApplicationProps {
    languages: LanguageItemDto[] | undefined;
}

const RenderApplication = (props: RenderApplicationProps): React.ReactElement => {
    const hasSnapshotMode = HasSnapshotMode();
    useXssWarning();

    return (
        <ApplicationSession>
            <BrowserRouter>
                <ClearPageStart>
                    <Switch>
                        <MapComponentsToRoutes languages={props.languages} />
                    </Switch>
                </ClearPageStart>
            </BrowserRouter>
            {hasSnapshotMode ? null : (
                <>
                    <ApplicationCookie />
                    <ApplicationToast />
                    <ApplicationDialogBox />
                    <ApplicationUserInfo />
                    <ScrollToTop />
                </>
            )}
        </ApplicationSession>
    );
};

const PrerenderedWrapper = (): React.ReactElement => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    return <RenderApplication languages={language.languages} />;
};

const App = (props: AppProps): React.ReactElement => {
    useUserData();
    useAnimation();

    if (!props.manifest) {
        /* Pre-rendered SPA */
        return <PrerenderedWrapper />;
    } else {
        /* Normal mode */
        useApplicationLanguage(props.manifest);
    }

    return <RenderApplication languages={props.manifest?.languages} />;
};

export default App;
