import * as React from "react";
import { useLocation } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Navigation from "../Components/Layout/navigation";
import Footer from "../Components/Layout/footer";
import ArticleList from "../Components/Articles/articleList";
import ArticleDetail from "../Components/Articles/articleDetail";
import { CustomColours } from "../Theme/customColours";
import { getFooterContent, getNavigationContent } from "../Api/Services";
import { footerDefault, navigationDefault } from "../Api/Defaults";

const useQuery = () => 
{
    return new URLSearchParams(useLocation().search);
}

export default function ArticlesPage() 
{
    const queryParam = useQuery();
    const id = queryParam.get("id");
    const content = id ? <ArticleDetail id={id} /> : <ArticleList />;
    const mountedRef = React.useRef(true);
    const [navigation, setNavigationContent] = React.useState({ data: navigationDefault, isLoading: true });
    const [footer, setFooterContent] = React.useState({ data: footerDefault, isLoading: true });

    const updateContent = React.useCallback(async () => 
    {
        if (!mountedRef.current) return;
        setNavigationContent({ data: await getNavigationContent(), isLoading: false });
        setFooterContent({ data: await getFooterContent(), isLoading: false });
    }, [ ]);

    React.useEffect(() => 
    {
        updateContent();
        return () => { mountedRef.current = false; };
    }, 
    [ updateContent ]);

    return (
        <>
            <Navigation navigation={navigation.data} isLoading={navigation.isLoading} />
            <Container>
                {content}
            </Container>
            <Footer footer={footer.data} isLoading={footer.isLoading} backgroundColor={CustomColours.background.gray1} />
        </>
    );
}
