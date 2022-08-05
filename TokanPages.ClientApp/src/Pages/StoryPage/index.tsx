import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { ActionCreators as StoryContent } from "../../Redux/Actions/Content/getStoryContentAction";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

export const StoryPage = (): JSX.Element => 
{ 
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const story = useSelector((state: IApplicationState) => state.getStoryContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(StoryContent.getStoryContent());
    }, 
    [ dispatch, language.id ]);
    
    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <DocumentContent content={story?.content} isLoading={story?.isLoading} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
