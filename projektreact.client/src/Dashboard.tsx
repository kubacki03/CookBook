
import React from "react";
import SearchRecipes from "./SearchRecipes";
import FoundRecipesList from "./FoundRecipesList";
import Logout from "./Logout";
import AddRecipe from "./AddRecipe";

const Dashboard: React.FC = () => {
    return (
        <div>
        <Logout/>
            <h1>Witaj na Dashboardzie!</h1>
            <SearchRecipes />
            <AddRecipe/>
        </div>
    );
};

export default Dashboard;
