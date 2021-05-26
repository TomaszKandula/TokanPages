import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import { REQUEST_TERMS } from "../Redux/Actions/getStaticContentAction";
import { IApplicationState } from "../Redux/applicationState";
import { combinedDefaults } from "../Redux/combinedDefaults";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";

export default function TermsPage() 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    const fetchNavigationContent = React.useCallback(() => dispatch(NavigationContent.getNavigationContent()), [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => dispatch(FooterContent.getFooterContent()), [ dispatch ]);

    React.useEffect(() => 
    { 
        if (navigation?.content === combinedDefaults.getNavigationContent.content) 
            fetchNavigationContent(); 
    }, [ fetchNavigationContent, navigation?.content ]);
    
    React.useEffect(() => 
    { 
        if (footer?.content === combinedDefaults.getFooterContent.content) 
            fetchFooterContent(); 
    }, [ fetchFooterContent, footer?.content ]);

    return (
        <>
            <Navigation navigation={navigation} isLoading={navigation?.isLoading} />
            <Container>
                <StaticContent content={REQUEST_TERMS} />
            </Container>
            <Footer footer={footer} isLoading={footer?.isLoading} />
        </>
    );
}
