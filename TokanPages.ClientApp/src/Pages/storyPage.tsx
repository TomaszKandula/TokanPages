import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import { REQUEST_STORY } from "../Redux/Actions/Content/getStaticContentAction";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/Content/getFooterContentAction";

const StoryPage = (): JSX.Element => 
{ 
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    
    return (
        <>
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <StaticContent content={REQUEST_STORY} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default StoryPage;
