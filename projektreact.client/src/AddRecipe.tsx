 import axios from "axios";
import React from "react";
import { useForm, useFieldArray } from 'react-hook-form';

type FormData = {
    Title: string;
    Description: string;
    Ingredients: Ingredient[];
};

type Ingredient = {
    IngredientName: string;
    Weight: number;
};

const RecipeForm: React.FC = () => {
    const {
        register,
        control,
        handleSubmit,
        formState: { errors },
    } = useForm<FormData>({
        defaultValues: {
            Ingredients: [{ IngredientName: '', Weight: 0 }],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: "Ingredients",
    });

    const onSubmit = async (data: FormData) => {
        console.log("Dane z formularza:", data);

        try {
            const response = await axios.post('/Recipe/AddRecipe', data);

          
            console.log("Odpowiedź:", response);
        } catch (error) {
            console.error("Błąd podczas dodawania przepisu:", error);
        }
    };


    return (
        <form onSubmit={handleSubmit(onSubmit)} className="mx-auto max-w-xl space-y-6 rounded-2xl bg-white p-6 shadow-lg">
            <div>
                <label className="mb-1 block text-sm font-medium">Nazwa przepisu</label>
                <input
                    type="text"
                    {...register("Title", { required: "Tytuł jest wymagany" })}
                    className="w-full rounded-xl border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                />
                {errors.Title && <p className="mt-1 text-sm text-red-500">{errors.Title.message}</p>}
            </div>

            <div>
                <label className="mb-1 block text-sm font-medium">Opis</label>
                <input
                    type="text"
                    {...register("Description", { required: "Opis jest wymagany" })}
                    className="w-full rounded-xl border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                />
                {errors.Description && <p className="mt-1 text-sm text-red-500">{errors.Description.message}</p>}
            </div>

            <div>
                <h3 className="mb-2 text-lg font-semibold">Składniki</h3>
                <div className="space-y-4">
                    {fields.map((field, index) => (
                        <div key={field.id} className="flex flex-col items-center gap-2 md:flex-row">
                            <input
                                placeholder="Nazwa składnika"
                                {...register(`Ingredients.${index}.IngredientName`, {
                                    required: "Nazwa składnika jest wymagana",
                                })}
                                className="flex-1 rounded-xl border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                            />
                            <input
                                type="number"
                                placeholder="Waga (g)"
                                {...register(`Ingredients.${index}.Weight`, {
                                    required: "Waga jest wymagana",
                                    valueAsNumber: true,
                                })}
                                className="w-32 rounded-xl border border-gray-300 px-4 py-2 focus:outline-none focus:ring-2 focus:ring-amber-500"
                            />
                            <button
                                type="button"
                                onClick={() => remove(index)}
                                className="text-red-500 hover:text-red-700 transition"
                            >
                                Usuń
                            </button>
                        </div>
                    ))}
                </div>

                <button
                    type="button"
                    onClick={() => append({ IngredientName: "", Weight: 0 })}
                    className="mt-4 bg-green-500 text-white px-4 py-2 rounded-xl hover:bg-green-600 transition"
                >
                    Dodaj składnik
                </button>
            </div>

            <button
                type="submit"
                className="w-full rounded-xl bg-amber-500 px-6 py-3 font-semibold text-white transition hover:bg-amber-600"
            >
                Zapisz przepis
            </button>
        </form>

    );
};

export default RecipeForm;