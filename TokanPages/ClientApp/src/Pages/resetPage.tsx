import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";
import { IApplicationState } from "../Redux/applicationState";
import { ActionCreators as NavigationContent } from "../Redux/Actions/getNavigationContentAction";
import { ActionCreators as FooterContent } from "../Redux/Actions/getFooterContentAction";
import { ActionCreators as ResetFormContent } from "../Redux/Actions/getResetFormContentAction";

export default function ResetPage() 
{
    const dispatch = useDispatch();
    
    const navigation = useSelector((state: IApplicationState) => state.getNavigationContent);
    const footer = useSelector((state: IApplicationState) => state.getFooterContent);
    const resetForm = useSelector((state: IApplicationState) => state.getResetFormContent);

    const getContent = React.useCallback(() =>
    {
        dispatch(NavigationContent.getNavigationContent());
        dispatch(FooterContent.getFooterContent());
        dispatch(ResetFormContent.getResetFormContent());
    }, [ dispatch ]);

    React.useEffect(() => getContent(), [ getContent ]);
    
    return (
        <>     
            <Navigation navigation={navigation} isLoading={navigation?.isLoading} />
            <Container>
                <ResetForm resetForm={resetForm} isLoading={resetForm?.isLoading} />
            </Container>
            <Footer footer={footer} isLoading={footer?.isLoading} />
        </>
    );
}
