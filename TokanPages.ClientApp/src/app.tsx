import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { BrowserRouter, Switch } from "react-router-dom";
import Fab from "@material-ui/core/Fab";
import KeyboardArrowUpIcon from "@material-ui/icons/KeyboardArrowUp";
import { ApplicationState } from "./Store/Configuration";
import { GetContentManifestDto, LanguageItemDto } from "./Api/Models";
import { UpdateReduxStore } from "./Shared/Services/languageService";
import { InitializeAnimations, EnsureUserData } from "./Shared/Services/initializeService";
import { HasSnapshotMode } from "./Shared/Services/SpaCaching";
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

interface Properties {
    manifest: GetContentManifestDto | undefined;
}

interface RenderApplicationProps {
    languages: LanguageItemDto[] | undefined;
}

const RenderApplication = (props: RenderApplicationProps): React.ReactElement => {
    const hasSnapshotMode = HasSnapshotMode();
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
                    <ScrollToTop>
                        <Fab size="small" aria-label="scroll back to top" className="button-up">
                            <KeyboardArrowUpIcon />
                        </Fab>
                    </ScrollToTop>
                </>
            )}
        </ApplicationSession>
    );
};

const PrerenderedWrapper = (): React.ReactElement => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    return <RenderApplication languages={language.languages} />;
};

const App = (props: Properties): React.ReactElement => {
    const dispatch = useDispatch();

    React.useEffect(() => {
        EnsureUserData(dispatch);
    }, []);

    React.useEffect(() => {
        const intervalId = InitializeAnimations();
        return () => clearInterval(intervalId);
    }, []);

    if (!props.manifest) {
        /* Pre-rendered SPA */
        return <PrerenderedWrapper />;
    } else {
        /* Normal mode */
        UpdateReduxStore(props.manifest, dispatch);
    }

    return <RenderApplication languages={props.manifest?.languages} />;
};

export default App;
