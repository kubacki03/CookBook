
import React from "react";
import SearchRecipes from "./SearchRecipes";

import Logout from "./Logout";
import AddRecipe from "./AddRecipe";


const Dashboard: React.FC = () => {
    return (
        <div>
            <header className="flex h-12 items-center gap-x-4 bg-amber-300 p-2">
                <Logout />
                <a className=" text-lg font-bold" href="/dashboard">Strona główna</a>
                <a className="text-lg font-bold" href="/recipes">Moje przepisy</a>
            </header>

            <SearchRecipes />
            <AddRecipe />
        </div>
    );
};

export default Dashboard;
