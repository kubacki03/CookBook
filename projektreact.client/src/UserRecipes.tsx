import axios from "axios";
import React from "react";

import { useEffect, useState } from "react";
import Logout from "./Logout";

type Ingredient = {
    ingredientName: string;
    weight: number;
};

type Recipe = {
    title: string;
    description: string;
    ingredients: Ingredient[];
};


function UserRecipes() {
    const [recipes, setRecipes] = useState<Recipe[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchUserRecipes = async () => {
            try {
                const token = localStorage.getItem("token");
                const response = await axios.get('http://localhost:5119/Recipe/GetUserRecipes', {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }

                });

                console.log(response.data);
                setRecipes(response.data);
            } catch (err: any) {
                setError("B��d podczas pobierania danych.");
                console.error(err);
            }
        };

        fetchUserRecipes();
    }, []);

    if (error) return <p>{error}</p>;

    return (
        <div >
            <header className="flex h-12 items-center gap-x-4 bg-amber-300 p-2">
                <Logout />
                <a className=" text-lg font-bold" href="/dashboard">Strona główna</a>
                <a className="text-lg font-bold" href="/recipes">Moje przepisy</a>
            </header>

            <h2 className="mb-4 text-2xl font-bold">Moje przepisy:</h2>

            {recipes.length === 0 ? (
                <p className="text-gray-500">Brak przepis�w.</p>
            ) : (
                <div className="space-y-6">
                    {recipes.map((recipe, index) => (
                        <div
                            key={index}
                            className="rounded-xl bg-white p-4 shadow-md transition hover:shadow-lg"
                        >
                            <h3 className="mb-2 text-xl font-semibold text-amber-700">{recipe.title}</h3>
                            <p className="mb-3 text-gray-700">{recipe.description}</p>
                            <ul className="list-inside list-disc text-gray-600">
                                {recipe.ingredients?.map((ingredient, i) => (
                                    <li key={i}>
                                        {ingredient.ingredientName}: {ingredient.weight}g
                                    </li>
                                ))}
                            </ul>
                        </div>
                    ))}
                </div>
            )}
        </div>

    );
}

export default UserRecipes;
