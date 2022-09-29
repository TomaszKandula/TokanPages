import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { IApplicationState } from "../../Store/Configuration";
import { Navigation, Footer } from "../../Components/Layout";
import { DocumentContent } from "../../Components/Document";

import { 
    GetNavigationContentAction, 
    GetFooterContentAction, 
    GetStoryContentAction 
} from "../../Store/Actions";

export const StoryPage = (): JSX.Element => 
{ 
    const dispatch = useDispatch();
    
    const language = useSelector((state: IApplicationState) => state.userLanguage);
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const story = useSelector((state: IApplicationState) => state.getStoryContent);

    React.useEffect(() => 
    {
        dispatch(GetNavigationContentAction.getNavigationContent());
        dispatch(GetFooterContentAction.getFooterContent());
        dispatch(GetStoryContentAction.getStoryContent());
    }, 
    [ dispatch, language?.id ]);
    
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
