import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import { REQUEST_POLICY } from "../../Redux/Actions/Content/getStaticContentAction";
import { IApplicationState } from "../../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../../Redux/Actions/Content/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../../Redux/Actions/Content/getFooterContentAction";
import { Navigation } from "../../Components/Layout";
import { Footer } from "../../Components/Layout";
import { StaticContent } from "../../Components/Content/staticContent";

const PolicyPage = (): JSX.Element => 
{
    const dispatch = useDispatch();

    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);

    React.useEffect(() => 
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
    }, 
    [ dispatch ]);

    return (
        <>     
            <Navigation content={navigation?.content} isLoading={navigation?.isLoading} />
            <Container>
                <StaticContent content={REQUEST_POLICY} />
            </Container>
            <Footer content={footer?.content} isLoading={footer?.isLoading} />
        </>
    );
}

export default PolicyPage;