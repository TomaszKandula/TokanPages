import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import NavigationView from "../Components/Layout/navigationView";
import StaticContent from "../Components/Content/staticContent";
import Footer from "../Components/Layout/footer";
import { REQUEST_TERMS } from "../Redux/Actions/getStaticContentAction";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";

export default function TermsPage() 
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
            <NavigationView content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <StaticContent content={REQUEST_TERMS} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}
