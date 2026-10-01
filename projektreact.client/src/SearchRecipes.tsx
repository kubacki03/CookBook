import React, { useState } from "react";
import axios from "axios";
import FoundRecipesList from "./FoundRecipesList";

const SearchRecipes: React.FC = () => {
    const [searchTerm, setSearchTerm] = useState("");
    const [recipes, setRecipes] = useState<any[]>([]);

    const handleSearch = async () => {
        if (!searchTerm) return;

        try {
            const response = await axios.get("/Recipe/GetRecipes", {
                params: { name: searchTerm }
            });

            setRecipes(response.data);
        } catch (error) {
            console.error("Error fetching recipes:", error);
        }
    };

    return (
        <div className="flex flex-col items-center gap-4 p-4 md:flex-row">
            <input
                type="text"
                placeholder="Wpisz nazwę przepisu..."
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                className="w-full md:w-80 px-4 py-2 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-amber-500"
            />
            <button
                onClick={handleSearch}
                className="rounded-xl bg-amber-500 px-6 py-2 text-white transition-colors hover:bg-amber-600"
            >
                Szukaj
            </button>

            <FoundRecipesList recipes={recipes} />
        </div>

    );
};

export default SearchRecipes;