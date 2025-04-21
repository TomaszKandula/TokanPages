import { ErrorContentDto } from "Api/Models";
import { useSelector } from "react-redux";
import { ApplicationState } from "Store/Configuration";

export const useErrorBoundaryContent = (): ErrorContentDto => {
    const language = useSelector((state: ApplicationState) => state.applicationLanguage);
    const filtering = (value: ErrorContentDto): boolean => {
        return value.language === language.id;
    };

    const result = language.errorBoundary.filter(filtering)[0];
    return result;
}
