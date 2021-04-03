import * as React from "react";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import ResetForm from "../Components/Account/resetForm";
import Footer from "../Components/Layout/footer";
import { footerContentDefault, navigationContentDefault, resetFormContentDefault } from "../Api/Defaults";
import { getFooterContent, getNavigationContent, getResetFormContent } from "../Api/Services";

export default function ResetPage() 
{
    const mountedRef = React.useRef(true);
    const [resetForm, setResetFormContent] = React.useState({ data: resetFormContentDefault, isLoading: true });
    const [navigation, setNavigationContent] = React.useState({ data: navigationContentDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerContentDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setResetFormContent({ data: await getResetFormContent(), isLoading: false });
        setNavigationContent({ data: await getNavigationContent(), isLoading: false });
        setFooterContent({ data: await getFooterContent(), isLoading: false });
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, [ updateContent ]);

    return (
        <>     
            <Navigation navigation={navigation.data} isLoading={navigation.isLoading} />
            <Container>
                <ResetForm resetForm={resetForm.data} isLoading={resetForm.isLoading} />
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} />
        </>
    );
}
