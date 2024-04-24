import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { ApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

import { ContentNavigationAction, ContentFooterAction, ContentStoryAction } from "../../Store/Actions";

export const StoryPage = (): JSX.Element => {
    const dispatch = useDispatch();
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const story = useSelector((state: ApplicationState) => state.contentStory);

    React.useEffect(() => {
        dispatch(ContentNavigationAction.get());
        dispatch(ContentFooterAction.get());
        dispatch(ContentStoryAction.get());
    }, [language?.id]);

    return (
        <>
            <Navigation />
            <Container>
                <DocumentContent content={story?.content} isLoading={story?.isLoading} />
            </Container>
            <Footer />
        </>
    );
};
