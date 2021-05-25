import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { combinedDefaults } from "../Redux/combinedDefaults";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as ResetFormContent } from "../Redux/Actions/getResetFormContentAction";

export default function ResetPage() 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const resetForm = useSelector((state: IApplicationState) => state.getResetFormContent);

    const fetchNavigationContent = React.useCallback(() => { dispatch(NavigationContent.getNavigationContent()); }, [ dispatch ]);
    const fetchFooterContent = React.useCallback(() => { dispatch(FooterContent.getFooterContent()); }, [ dispatch ]);
    const fetchResetFormContent = React.useCallback(() => { dispatch(ResetFormContent.getResetFormContent()); }, [ dispatch ]);

    React.useEffect(() => { if (navigation.content === combinedDefaults.getNavigationContent.content) fetchNavigationContent(); }, [ fetchNavigationContent, navigation.content ]);
    React.useEffect(() => { if (footer.content === combinedDefaults.getFooterContent.content) fetchFooterContent(); }, [ fetchFooterContent, footer.content ]);
    React.useEffect(() => { if (resetForm.content === combinedDefaults.getResetFormContent.content) fetchResetFormContent(); }, [ fetchResetFormContent, resetForm.content ]);
    
    return (
        <>     
            <Navigation navigation={navigation} isLoading={navigation.isLoading} />
            <Container>
                <ResetForm resetForm={resetForm} isLoading={resetForm.isLoading} />
            </Container>
            <Footer footer={footer} isLoading={footer.isLoading} />
        </>
    );
}
